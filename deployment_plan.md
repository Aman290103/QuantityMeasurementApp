# Deployment Implementation Plan

This plan outlines the steps to migrate the database from SQL Server to PostgreSQL and deploy the application to Vercel (Frontend) and Render (Backend).

## Phase 1: Backend Migration (SQL Server to PostgreSQL)

1.  **Dependencies**:
    - Install `Npgsql.EntityFrameworkCore.PostgreSQL` in `QuantityMeasurementApp.WebApi` and `QuantityMeasurementApp.Repository`.
    - (Optional) Remove `Microsoft.EntityFrameworkCore.SqlServer`.
2.  **Configuration**:
    - Update `Program.cs` to use `UseNpgsql` instead of `UseSqlServer`.
    - Update `appsettings.json` with a PostgreSQL connection string template.
3.  **Migrations**:
    - Remove existing SQL Server migrations (if any) and create new ones for PostgreSQL to ensure compatibility.

## Phase 2: Backend Deployment Preparation (Render)

1.  **Dockerfile**:
    - Create a `Dockerfile` in the root (or `WebApi` directory) for a multi-stage build.
2.  **Environment Variables**:
    - Ensure the connection string and JWT settings can be overridden by environment variables on Render.

## Phase 3: Frontend Deployment Preparation (Vercel)

1.  **Environment Configuration**:
    - Update Angular services to use an environment-based API URL instead of hardcoded `127.0.0.1`.
    - Create `vercel.json` if needed (standard Angular apps usually don't need much, but it's good for clean URLs).
2.  **Build Configuration**:
    - Ensure the build command `npm run build` is ready.

## Phase 4: Execution

1.  Apply code changes.
2.  User to set up Render (Web Service + PostgreSQL).
3.  User to set up Vercel (Import Git repo).
