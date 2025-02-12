# **NetworkChat**
![.NET](https://img.shields.io/badge/.NET-512BD4?logo=dotnet&logoColor=fff) ![.NET8](https://img.shields.io/badge/8.0-%23512BD4?logo=.NET
) ![forks](https://img.shields.io/github/forks/JordanOleg/NetworkChat?style=flat) ![issues](https://img.shields.io/github/issues/JordanOleg/NetworkChat?style=flat&logoColor=grey&color=blue) ![starsRepo](https://img.shields.io/github/stars/JordanOleg/NetworkChat?style=flat) 

---

**Network Chat** is a simple console chat application developed on the .NET 6 platform. The project consists of three main components: `NetworkLibrary`, `UserClient`, and `UserServer`. This chat allows users to exchange messages over a network, where the server is responsible for broadcasting messages to all connected clients.

## Project Structure

The project is divided into three separate solutions:

1. **NetworkLibrary**  
   This is the core library that contains classes for network operations. It handles basic functionality such as network connection, sending, and receiving data. This library is used in both the client and server components.

2. **UserClient**  
   This is the client-side application that represents a user. Each client connects to the server and can send messages, which are then relayed to all other connected clients.

3. **UserServer**  
   This is the server-side application responsible for managing connected clients and broadcasting messages. When a client sends a message, the server forwards it to all other clients.

## How It Works

1. The **Server** starts and waits for client connections.
2. **Clients** connect to the server and can begin exchanging messages.
3. When a client sends a message, the server receives it and forwards it to all other connected clients.
4. Each client receives the message and displays it in the console.

## Supported Protocols
1. **TCP**
2. **UDP**
3. *Ability to add custom protocols*

## Requirements

- .NET SDK
- IDE (e.g., Visual Studio 2022 or JetBrains Rider)

## How to Run the Project

1. Clone the repository:
   ```bash
   git clone https://github.com/JordanOleg/NetworkChat.git
   ```
2. Open the solution in your IDE.
3. First, run the **UserServer** project to start the server.
4. Then, run one or more instances of **UserClient** to connect clients to the server.
5. Enter a message in the client console, and it will be sent to all other clients via the server.

## Usage Example

1. **Starting the Server:**
   ```bash
   cd UserServer
   dotnet run
   ```
   The server will start and wait for client connections.

2. **Starting a Client:**
   ```bash
   cd UserClient
   dotnet run
   ```
   After connecting to the server, enter a message in the console, and it will be sent to all other clients.

## License

This project is distributed under the MIT License. For more details, see the [LICENSE](LICENSE) file.

## Author

- **JordanOleg**  
  [GitHub](https://github.com/JordanOleg)

---

If you have any questions or suggestions for improving the project, please create an issue or submit a pull request in the [repository](https://github.com/JordanOleg/NetworkChat). I welcome your contributions!