# HelpDeskPro

Issue reporting and management system for organizations with multiple branches.

This repository contains:
- **backend/**: ASP.NET Core Web API (JWT authentication)
- **frontend/**: React (Create React App) web app for management/admins (work in progress)

---

## Features (high level)

- Multi-branch issue/ticket reporting and tracking
- Role-based access (typical roles: user, IT staff, managers, admin)
- Issue classification by **status**, **priority**, and **category**
- Attachments support (images/files/recordings) *(depends on backend implementation)*

---

## Backend (ASP.NET Core Web API)

Location: `backend/`

### Requirements
- .NET SDK (version depends on your project settings in `backend/backend.csproj`)

### Run the API
From the repository root:

```bash
cd backend
# using the project file
 dotnet run
```

By default (Development), launch settings specify:
- HTTP: `http://localhost:5021`
- HTTPS: `https://localhost:7225`

### OpenAPI / Swagger
This API maps OpenAPI in Development via `app.MapOpenApi()`.
Once running in Development, the OpenAPI endpoint is available (exact path depends on ASP.NET configuration).

---

## Frontend (React Admin/Management Portal)

Location: `frontend/`

> Note: The React project is still under development.

### Requirements
- Node.js (LTS recommended)
- npm

### Install & run

```bash
cd frontend
npm install
npm start
```

Default dev server:
- `http://localhost:3000`

---

## Mobile App

The companion mobile app lives in a separate repository:
- `DanushkaMadush/HelpDeskPro-Mobile`

---

## Realtime Notifications (SignalR)

The backend exposes a SignalR hub at `/notificationHub` for realtime in-app toast notifications.

### How it works

- On connect, each authenticated user is added to a group named `user_{userId}` (where `userId` is the `ClaimTypes.NameIdentifier` from the JWT).
- The backend emits the event **`ReceiveNotification`** with a `NotificationMessage` payload:

```json
{
  "title": "string",
  "message": "string",
  "ticketId": 123,
  "systemId": 456,
  "statusId": 2,
  "createdAt": "2024-01-01T00:00:00Z"
}
```

**Triggered events:**
- **Ticket Created** → `ReceiveNotification` sent to all developers assigned to the ticket's system.
- **Ticket Status Updated** → `ReceiveNotification` sent to the ticket requester (`CreatedBy`).

### Configuring the Hub URL (local dev)

| Setting | Value |
|---|---|
| REST API base | `http://<host>:5021/api/v1` |
| SignalR hub | `http://<host>:5021/notificationHub` |

Replace `<host>` with your machine's LAN IP (e.g. `192.168.8.104`) so the mobile device on the same network can connect.

### Mobile client setup (`HelpDeskPro-Mobile`)

See the [mobile repository](https://github.com/DanushkaMadush/HelpDeskPro-Mobile) for the full Expo/React Native implementation. The key steps are:

1. **Install dependencies:**
   ```bash
   npm install @microsoft/signalr react-native-toast-message
   ```
2. **Set the hub URL in `src/api/config.ts`:**
   ```ts
   export const API_CONFIG = {
     BASE_URL: 'http://192.168.8.104:5021/api/v1',
     HUB_URL: 'http://192.168.8.104:5021/notificationHub',
   };
   ```
3. **Create `src/realtime/notificationsHub.ts`** – a module that builds a `HubConnection` using `accessTokenFactory: () => getToken()`, enables automatic reconnect, and subscribes to `ReceiveNotification` to show toast banners.
4. **Wire the `<Toast />` component** at the root of `app/_layout.tsx` and call `startNotificationHub()` once the user is authenticated (after login) and `stopNotificationHub()` on logout.

---

## Contributing

1. Create a feature branch
2. Commit changes
3. Open a pull request

---

## License

See [`LICENSE`](./LICENSE).