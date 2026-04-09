# Mobile App Changes Required

**Target repo:** `DanushkaMadush/HelpDeskPro-Mobile`
**Target branch:** `copilot/featurerealtime-notifications`

## Status of each file

| File | Status |
|------|--------|
| `package.json` | ✅ Already has `@microsoft/signalr` (^10.0.0) and `react-native-toast-message` (^2.3.3) |
| `src/api/config.ts` | ✅ Already has `HUB_URL` |
| `src/realtime/notificationsHub.ts` | ❌ Missing — create this file |
| `app/_layout.tsx` | ⚠️ Needs update — currently uses `useNotificationHub` hook instead of direct `startNotificationHub` call |

---

## File: src/realtime/notificationsHub.ts

**Action:** Create new file

```ts
import * as signalR from '@microsoft/signalr';
import Toast from 'react-native-toast-message';
import { API_CONFIG } from '../api/config';
import { getToken } from '../utils/tokenStorage';

export type NotificationMessage = {
  title: string;
  message: string;
  ticketId?: number;
  systemId?: number;
  statusId?: number;
  createdAt: string;
};

let connection: signalR.HubConnection | null = null;

export function getNotificationHubConnection(): signalR.HubConnection {
  if (!connection) {
    connection = new signalR.HubConnectionBuilder()
      .withUrl(API_CONFIG.HUB_URL, {
        accessTokenFactory: () => getToken().then((t) => t ?? ''),
      })
      .withAutomaticReconnect()
      .configureLogging(signalR.LogLevel.Warning)
      .build();

    connection.on('ReceiveNotification', (payload: NotificationMessage) => {
      Toast.show({
        type: 'info',
        text1: payload.title,
        text2: payload.message,
        visibilityTime: 4000,
        position: 'top',
      });
    });
  }
  return connection;
}

export async function startNotificationHub(): Promise<void> {
  const conn = getNotificationHubConnection();
  if (
    conn.state === signalR.HubConnectionState.Disconnected
  ) {
    try {
      await conn.start();
    } catch (err) {
      console.warn('[SignalR] Failed to start connection:', err);
    }
  }
}

export async function stopNotificationHub(): Promise<void> {
  if (
    connection &&
    connection.state !== signalR.HubConnectionState.Disconnected
  ) {
    try {
      await connection.stop();
    } catch (err) {
      console.warn('[SignalR] Failed to stop connection:', err);
    }
  }
}
```

---

## File: app/_layout.tsx

**Action:** Replace entire file content

```tsx
import { DarkTheme, DefaultTheme, ThemeProvider } from '@react-navigation/native';
import { Stack } from 'expo-router';
import { StatusBar } from 'expo-status-bar';
import { useEffect } from 'react';
import 'react-native-reanimated';
import { SafeAreaProvider } from 'react-native-safe-area-context';
import Toast from 'react-native-toast-message';

import { useColorScheme } from '@/hooks/use-color-scheme';
import { startNotificationHub, stopNotificationHub } from '@/src/realtime/notificationsHub';
import { getToken } from '@/src/utils/tokenStorage';

export default function RootLayout() {
  const colorScheme = useColorScheme();

  useEffect(() => {
    let mounted = true;

    (async () => {
      const token = await getToken();
      if (mounted && token) {
        await startNotificationHub();
      }
    })();

    return () => {
      mounted = false;
      stopNotificationHub();
    };
  }, []);

  return (
    <SafeAreaProvider>
      <ThemeProvider value={colorScheme === 'dark' ? DarkTheme : DefaultTheme}>
        <Stack screenOptions={{ headerShown: false }}>
          <Stack.Screen name="(auth)" />
          <Stack.Screen name="(tabs)" />
          <Stack.Screen
            name="modal"
            options={{ presentation: 'modal', title: 'Modal' }}
          />
        </Stack>
        <StatusBar style="auto" />
      </ThemeProvider>
      <Toast />
    </SafeAreaProvider>
  );
}
```

---

## Notes

- `src/hooks/useNotificationHub.ts` and `src/realtime/notificationClient.ts` can be kept
  (they are not imported by the updated `_layout.tsx`) or deleted as cleanup.
- The `NotificationMessage` type in `notificationsHub.ts` is the full payload shape
  from the backend (`ticketId`, `systemId`, `statusId`, `createdAt`) and supersedes
  the narrower type in `notificationClient.ts`.
