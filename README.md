# Docker Assignment

# Run project

Run the following command to into "rabbitmq-docker-sample" directory

Create a bridge network 

```shell
docker network create --driver=bridge --subnet=172.16.0.0/16 rabbit_network
```
Build and run the conatiners

```shell
docker-compose build
docker-compose up
```

# Publish data

Run the follwing command into curl to publish the data 

```shell
curl -H "Content-Type: application/json" -X POST -d '{"Name":"Nazmul","Address":"Dhaka Bangladesh"}' http://localhost:8000/api/publication
```
# Get data
Hit following url into web browser/curl
http://localhost:8080/api/person
