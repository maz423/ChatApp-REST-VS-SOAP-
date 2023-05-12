Note: Volumes in docker-compose.yml need to be updated to your local machine.

How to run:

cd to directory:

docker-compose up

in a different terminal:

docker attach mono_1

cd code

mcs -r:System.Net client.cs

mono client.exe

follow prompts on screen.
