# OpenArtspaceGallery
Opensource gallery


# Docker

1) Create virtual network

docker network create -d bridge gallery-app-net

2) Create PostgreSQL image

docker build -f dockerfile-gallery-app-postgresql -t gallery-app-infrastructure-postgresql .

3) Run infrastructure container

// This step is not working
Test:
docker-compose -f docker-compose-gallery-app-infrastructure.yml up

Run:
docker-compose -f docker-compose-gallery-app-infrastructure.yml up -d

--

Stop containers

1) List containers:

docker container ls

2) Stop container:

docker-compose -f docker-compose-infrastructure-gallery-app.yml down

