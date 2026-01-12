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

## Usage

```bash
shell-chat
```

## Development

```bash
dotnet build
dotnet run
```
