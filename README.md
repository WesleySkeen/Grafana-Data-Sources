# Grafana-Data-Sources

### Build and run the api with docker on local port 4000
```shell
> docker build --pull --no-cache -t weather_forcast_api -f ops/docker/Dockerfile .
> docker run -p 4000:5000 --rm weather_forcast_api
```

You should now be able to view OTEL metrics from the API

```shell
> curl http://localhost:4000/metrics

# TYPE process_runtime_dotnet_gc_collections_count counter
# HELP process_runtime_dotnet_gc_collections_count Number of garbage collections that have occurred since process start.
process_runtime_dotnet_gc_collections_count{generation="gen2"} 0 1665568680770
process_runtime_dotnet_gc_collections_count{generation="gen1"} 0 1665568680770
process_runtime_dotnet_gc_collections_count{generation="gen0"} 0 1665568680770

.
.
.

# EOF
```
