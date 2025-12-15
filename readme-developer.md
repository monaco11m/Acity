Reto Técnico Fullstack Senior

Este proyecto implementa una arquitectura de microservicios con .NET, RabbitMQ, SeaweedFS y un frontend en React (Vite).

Existen dos formas de ejecutar el sistema:

Escenario 1: Backend local + RabbitMQ y SeaweedFS en Docker

Este escenario es útil para desarrollo local, debugging y pruebas rápidas.

1. Levantar RabbitMQ (local en Docker)
docker rm -f rabbitmq-local; docker run -d --name rabbitmq-local -p 5672:5672 -p 15672:15672 rabbitmq:3-management


Puertos:

5672 → conexión AMQP

15672 → panel de administración (http://localhost:15672)

Usuario: guest

Password: guest

2. Levantar SeaweedFS (local en Docker)
docker rm -f seaweedfs-local; docker run -d --name seaweedfs-local -p 9333:9333 -p 8080:8080 chrislusf/seaweedfs server -dir=/data


Puertos:

9333 → Master

8080 → Volume server (subida/descarga de archivos)

3. Ejecutar los microservicios .NET localmente

Desde Visual Studio o consola:

Auth.Api

Control.Api

MassLoad.Api

Notification.Api

Gateway.Api

Todos deben apuntar a:

RabbitMQ → localhost:5672

SeaweedFS → localhost:9333 / localhost:8080

PostgreSQL → local o dockerizado según tu configuración

4. Ejecutar el frontend localmente
cd frontend
npm install
npm run dev


Frontend disponible en:

http://localhost:5173

Escenario 2: Orquestación completa con Docker Compose

Este escenario levanta todo el backend orquestado, listo para evaluación o demo.

Servicios incluidos

PostgreSQL

RabbitMQ

SeaweedFS

Auth API

Control API

MassLoad API

Notification API

Gateway API

Comando único para levantar todo

Desde la raíz del proyecto:

docker compose up --build

Puertos utilizados
Servicio	Puerto
Gateway API	5080
PostgreSQL	5432
RabbitMQ	5672
RabbitMQ UI	15672
SeaweedFS Master	9333
SeaweedFS Volume	8080

Gateway expone todos los endpoints públicos:

http://localhost:5080


Swagger (ejemplo):

http://localhost:5080/swagger

Frontend en este escenario

El frontend no está dockerizado por decisión técnica (rapidez y simplicidad).

Debe ejecutarse localmente:

cd frontend
npm install
npm run dev


Frontend:

http://localhost:5173


El frontend consume el backend vía:

http://localhost:5080

Notas finales

Los archivos se almacenan en SeaweedFS

El procesamiento masivo es asíncrono con RabbitMQ

Las notificaciones se envían vía Mailtrap

El historial y estados se gestionan desde Control Service