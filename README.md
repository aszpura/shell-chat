# shell-chat

A command-line interface tool for communicating with LLM models.

## Overview

shell-chat is a .NET CLI application that enables direct interaction with Large Language Models from your terminal.

## Features

- Direct CLI communication with LLM models
- Simple command-based interface

## Planned Features

- File context support
- MCP (Model Context Protocol) server integration

## Requirements

- .NET 10.0 or higher

## Installation

```bash
dotnet pack -c Release
dotnet tool install --global --add-source ./nupkg shell-chat
```

## Update

```bash
dotnet pack -c Release
dotnet tool update --global --add-source ./nupkg shell-chat
```

Alternatively, uninstall and reinstall:

```bash
dotnet tool uninstall --global shell-chat
dotnet pack -c Release
dotnet tool install --global --add-source ./nupkg shell-chat
```

## Uninstall

```bash
dotnet tool uninstall --global shell-chat
```

## Usage

```bash
shc
shc --help
```

## API Key Configuration

shell-chat requires an API key to communicate with LLM providers. The key can be configured using three methods (in priority order):

### 1. Command-line argument (highest priority)
```bash
shc --api-key YOUR_KEY query "your question"
shc -k YOUR_KEY q "your question"
```

### 2. Environment variable
```bash
# Windows
set SHELLCHAT_API_KEY=YOUR_KEY

# Linux/macOS
export SHELLCHAT_API_KEY=YOUR_KEY
```

### 3. Config file (persistent storage)
```bash
shc config set-key YOUR_KEY
```

The config file is stored at:
- Windows: `%APPDATA%\shell-chat\config.json`
- Linux/macOS: `~/.config/shell-chat/config.json`

### Configuration Commands

```bash
# Show current configuration
shc config show

# Set API key
shc config set-key YOUR_KEY

# Clear API key
shc config clear-key
```

**Note:** The config file stores the API key in plain text. Ensure appropriate file permissions.

## Query Command

Send queries to the LLM using the `query` command (or its alias `q`):

```bash
# Full command
shc query "What is the capital of France?"

# Using alias
shc q "Explain recursion in simple terms"

# With API key
shc query "Your question" --api-key YOUR_KEY
shc q "Your question" -k YOUR_KEY
```

## Quick Redeploy

For development, use the redeploy script to quickly rebuild and reinstall the tool:

```bash
# Windows
tools\redeploy.cmd
```

## Development

```bash
dotnet build
dotnet run
```
