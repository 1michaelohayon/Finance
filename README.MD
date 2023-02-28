mongoDB Setup:

1. move to mongodb dir and `docker compose -f docker-compose.dev.yml up`
2. get the container id with `docker container ls`
3. enter the container with `docker exec -it <id> bash`
4. enter mongodb CLI with `mongosh -u root -p sisma`
5. `use admin`
6. create user `db.createUser({user: "api", pwd: "sisma", roles: [{role: "readWrite", db: "Finance"}]});`

7. mongodb connection string : `mongodb://api:sisma@localhost:3456`

Tests command:
dotnet test --environment AUTH0_URL="" --environment AUTH0_AUDIENCE="" --environment AUTH0_CLIENT_ID="" --environment AUTH0_CLIENT_SECRET="" --environment BASE_URL=""
