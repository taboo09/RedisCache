## setup Redis via Docker

### create container
docker run --name my-redis -p 5012:6379 -d redis

### docker see images
docker ps -a

### use redis cmd
docker exec -it my-redis sh\
redis-cli\
ping\
select 0\
dbsize\
scan 0
