# Grafana-Data-Sources

### Build and run the api with docker on local port 4000
```shell
> docker build --pull --no-cache -t weather_forcast_api -f ops/docker/Dockerfile .
> docker run -p 4000:5000 --rm weather_forcast_api
```
