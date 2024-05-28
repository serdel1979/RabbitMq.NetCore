# README

## Ejecutar RabbitMQ con Docker

Este documento describe cómo ejecutar RabbitMQ utilizando Docker

### Prerrequisitos

- Tener instalado Docker en tu máquina.
- Tener acceso a una terminal o línea de comandos.

### Pasos para ejecutar RabbitMQ con Docker

1. **Descargar y ejecutar la imagen de RabbitMQ:**

   Ejecuta el siguiente comando para descargar y ejecutar RabbitMQ con la consola de administración habilitada:

   ```bash
   docker run -d --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3-management