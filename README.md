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

## Contributing

1. Create a feature branch
2. Commit changes
3. Open a pull request

---

## License

See [`LICENSE`](./LICENSE).