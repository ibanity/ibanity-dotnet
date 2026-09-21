# Rotating the NuGet API Key for GitHub Actions Publishing

This document explains how to regenerate the NuGet API key used to publish the
`Ibanity` package, and how to update it in the GitHub repository so the
publishing workflow keeps working.

## Overview

The GitHub Actions workflow that publishes the NuGet package authenticates to
nuget.org using an API key. That key is stored as a repository secret named
**`NUGET_API_KEY`**. When the key expires (or needs to be rotated for
security reasons), you need to:

1. Generate a new key on nuget.org.
2. Copy it (nuget.org only shows it once).
3. Update the `NUGET_API_KEY` secret in GitHub with the new value.

## Prerequisites

- Access to the nuget.org account that owns the `ibanity` package, with
  permission to manage its API keys.
- Admin (or "manage secrets") access to the `ibanity/ibanity-dotnet`
  repository on GitHub.
- A way to confirm your GitHub identity when prompted (GitHub Mobile app,
  an authenticator app, or your password).

## Step-by-step procedure

### 1. Regenerate the API key on nuget.org

1. Go to [nuget.org](https://www.nuget.org) and sign in.
2. Click your username in the top-right corner, then select **API Keys**
   (marked *"Not recommended"*).
3. This actually opens the **Trusted Publishing** page instead of the API
   Keys page (nuget.org nudges you toward Trusted Publishing these days).
   In the notice at the top, click the **API keys** link — the sentence
   reads *"...For publishing from command line or unsupported CI/CD
   workflows, **API keys** continue to work."* This takes you to the real
   API Keys management page.
4. Under **Manage**, find the key named **GitHub** (the one used for
   publishing, scoped to *"Push only new package versions"* for the
   `ibanity` package).
5. Click **Regenerate**.
6. Confirm by clicking **OK** in the "Are you sure you want to regenerate
   the API key?" dialog.
7. A banner appears: *"Your API key has been regenerated. Make sure to
   copy your new API key now using the Copy button below. You will not be
   able to do so again."*
8. Click **Copy** right away to copy the new key to your clipboard.

   > ⚠️ This is your only chance to copy the key. If you navigate away
   > before copying it, you'll need to regenerate it again.

### 2. Update the secret in GitHub

1. Go to the repository: `https://github.com/ibanity/ibanity-dotnet`.
2. Open **Settings → Secrets and variables → Actions**.
3. Under **Repository secrets**, find **`NUGET_API_KEY`** and click the
   pencil (edit) icon next to it.
4. In the **Value** field, paste the new key you copied from nuget.org.
5. Click **Update secret**.
6. If GitHub asks you to confirm your identity ("Confirm access"), approve
   it via GitHub Mobile, your authenticator app, or your password.
7. You should see a confirmation that the secret was updated, and the
   **Last updated** column for `NUGET_API_KEY` will show "now".

## Done

The next time the publishing workflow runs, it will use the new API key
automatically — no changes are needed in the workflow file itself.

## Security notes

- Never paste the API key anywhere other than the GitHub secret field
  (not in chat, code, issues, or documentation).
- The old key is invalidated as soon as you regenerate it, so the update
  on the GitHub side should be done promptly to avoid publishing failures.
- Treat the key like a password: if you ever suspect it has been exposed,
  regenerate it immediately by repeating this procedure.
