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

## Development

```bash
dotnet build
dotnet run
```
