# MoodBloom

[![Build Status](https://img.shields.io/github/actions/workflow/status/kenlacroix/MoodBloom/ci.yml?branch=main)](https://github.com/kenlacroix/MoodBloom/actions)  
[![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg)](LICENSE)

A privacy‑focused .NET 8 WinForms gratitude journal with AES‑256 encryption, MaterialSkin.2 UI, and zero‑knowledge cloud backup.

---

## Table of Contents

- [Overview](#overview)  
- [Features](#features)  
- [Tech Stack](#tech-stack)  
- [Getting Started](#getting-started)  
  - [Prerequisites](#prerequisites)  
  - [Installation](#installation)  
  - [Run the App](#run-the-app)  
- [Project Structure](#project-structure)  
- [Documentation](#documentation)  
- [Contributing](#contributing)  
- [License](#license)  
- [Authors & Acknowledgements](#authors--acknowledgements)  

---

## Overview

MoodBloom is a designer‑file‑free .NET 8 WinForms app (built in Visual Studio 2022) that lets you record timestamped gratitude entries (mood, tags, free text) in an AES‑encrypted JSON store under `%AppData%\MoodBloom`. MaterialSkin.2 provides a modern light/dark theming layer. An optional zero‑knowledge sync to any S3‑compatible service (e.g., DigitalOcean Spaces) keeps your data backed up without exposing content.

---

## Features

- **First‑Run Wizard**: Create user profile, choose password, and set storage path  
- **AES‑256 Encryption**: Local PBKDF2‑derived root key, secure JSON storage  
- **Journal UI**: Select mood, enter tags (with autocomplete), and write entries  
- **Preferences**: Change storage path, rotate password, view logs, export/wipe data, theme toggle  
- **Analytics & AI Insights**: Mood/tag charts & optional OpenAI‑powered writing prompts  
- **Cloud Sync**: Zero‑knowledge backup to S3‑compatible providers  
- **Logging & Error Handling**: Rolling logs with NLog, unhandled‑exception capture  
- **Accessibility & Localization**: Keyboard navigation, easy translation stubs  

---

## Tech Stack

- **Language**: VB.NET (no designers)  
- **Framework**: .NET 8.0‑windows, WinForms  
- **UI**: MaterialSkin.2  
- **Encryption**: AES‑256 via `EncryptionService` (PBKDF2 key derivation)  
- **Storage**: JSON + AES, `FileStorageService`  
- **Logging**: NLog (rolling logs)  
- **Tests**: xUnit/MSTest in `MoodBloom.Tests`  
- **CI**: GitHub Actions (`dotnet build`, `dotnet test`)  
- **Cloud**: S3‑compatible via `CloudSyncService`  

---

## Getting Started

### Prerequisites

- Windows 10+  
- Visual Studio 2022 with **.NET 8 SDK**  
- Git CLI or GitHub Desktop  

### Installation

1. **Clone the repo**  
   ```bash
   git clone https://github.com/kenlacroix/MoodBloom.git
   cd MoodBloom
