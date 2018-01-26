# Docker Assignment

# Run project

Run the following command to into "rabbitmq-docker-sample" directory

docker-compose build
docker-compose up

# Publish data

Run the follwing command into curl to publish the data
curl -d '{"Name":"Nazmul", "Address":"Dhaka, Bangladesh"}' -H "Content-Type: application/json" -X POST http://localhost:8000/api/publication

# Get data
Hit following url into web browser/curl
http://localhost:8080/api/person
