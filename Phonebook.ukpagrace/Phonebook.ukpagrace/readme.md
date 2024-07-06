# Application Setup Instructions

## Introduction

This guide provides instructions on how to set up the `appsettings.json` file required for the application to run properly.

## Prerequisites

Ensure you have the following information ready:
- Your database server name (e.g., your desktop name)
- Your database name
- A Gmail app password (follow the steps below to generate one)

## Steps to Generate a Gmail App Password

1. Go to the [Google Account Security page](https://myaccount.google.com/security).
2. Under "Signing in to Google," click on "2-Step Verification" and follow the steps to set it up if you haven't already.
3. Once 2-Step Verification is set up, go back to the Security page.
4. Click on "App passwords."
5. Select "Mail" as the app and "Other" as the device, then enter a custom name if desired.
6. Click on "Generate" to get your app password.

## Creating the `appsettings.json` File

Create a file named `appsettings.json` in the root directory of your project with the following structure:

```json
{
  "Database": {
    "Server": "your-desktop-name",
    "DatabaseName": "your-database-name"
  },
  "Email": {
    "Password": "your-gmail-app-password"
  }
}
```