Note: Volumes in docker-compose.yml need to be updated to your local machine.

How to run:

cd to directory:

docker-compose up

in a different terminal:

docker attach mono_2

cd code

mcs -r:System.Web.Services Client.cs MathService.cs

mono Client.exe

follow prompts on screen.
