# HelpDeskPro-Mobile – Realtime Notifications Implementation Guide

**Target repository:** `DanushkaMadush/HelpDeskPro-Mobile`  
**Target branch:** `copilot/featurerealtime-notifications`

> The branch already contains most of the required changes. This document lists what is implemented and what still needs to be done.

---

## Already implemented on branch `copilot/featurerealtime-notifications`

| File | What was added |
|------|---------------|
| `package.json` | `@microsoft/signalr` and `react-native-toast-message` dependencies |
| `src/api/config.ts` | `HUB_URL: 'http://192.168.8.104:5021/notificationHub'` |
| `src/realtime/notificationClient.ts` | Singleton `HubConnection` factory; subscribes to `ReceiveNotification` → `Toast.show()`; exports `startNotificationConnection` / `stopNotificationConnection` |
| `src/hooks/useNotificationHub.ts` | React hook that starts the hub on mount (if a token is present) and stops it on unmount |
| `app/_layout.tsx` | Mounts `<NotificationProvider>` (calls `useNotificationHub`) and renders `<Toast />` at the root |

---

## Remaining change: start connection after login

The `useNotificationHub` hook only runs once when `_layout.tsx` mounts. If the user is already logged in when the app starts, the connection starts automatically. However, **when a user logs in for the first time in a session** (no token in storage yet), the hook has already run and will not reconnect.

### Fix: update `app/(auth)/login.tsx`

After `saveToken(response.token)` succeeds, call `startNotificationConnection()` so the hub starts immediately after login without requiring an app restart.

```tsx
// At the top of login.tsx, add:
import { startNotificationConnection } from '@/src/realtime/notificationClient';

// Inside handleLogin, after await saveToken(response.token):
await startNotificationConnection();
```

Full updated `handleLogin`:

```tsx
const handleLogin = async () => {
  if (!email || !password) {
    Alert.alert('Error', 'Please enter both email and password');
    return;
  }

  setLoading(true);
  try {
    const response = await login({ email, password });
    await saveToken(response.token);
    await startNotificationConnection();          // <-- add this line

    const decoded = decodeToken(response.token);
    const role =
      decoded?.[
        'http://schemas.microsoft.com/ws/2008/06/identity/claims/role'
      ]?.toLowerCase();

    if (role === 'developer') {
      router.replace('/(tabs)/home-developer');
    } else {
      router.replace('/(tabs)/home-user');
    }
  } catch (error: any) {
    // ... existing error handling unchanged
  } finally {
    setLoading(false);
  }
};
```

---

## Optional: update `NotificationMessage` type in `notificationClient.ts`

The backend now sends `ticketId` and `statusId` in the payload. Add these optional fields to the type for future use (e.g. tapping a toast to navigate to the ticket):

```ts
export type NotificationMessage = {
  title: string;
  message: string;
  ticketId?: number;
  systemId?: number;
  statusId?: number;
  createdAt?: string;
};
```

---

## How to run locally

1. Ensure the backend is running on `http://192.168.8.104:5021` (or update `HUB_URL` in `src/api/config.ts` to match your machine's LAN IP).
2. Install dependencies: `npm install`
3. Start the app: `npx expo start`
4. Log in → you should see realtime toast banners when:
   - A ticket is created for a system you are assigned to (developer role)
   - The status of a ticket you created is updated (requester role)
