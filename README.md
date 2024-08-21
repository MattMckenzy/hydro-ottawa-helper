[<img alt="GitHub Release" src="https://img.shields.io/github/v/release/mattmckenzy/hydro-ottawa-helper?logo=github&color=forestgreen">](https://github.com/mattmckenzy/hydro-ottawa-helper)
[<img alt="Docker Release" src="https://img.shields.io/docker/v/mattmckenzy/hydro-ottawa-helper/latest?logo=docker">](https://hub.docker.com/r/mattmckenzy/hydro-ottawa-helper)

# Hydro Ottawa Helper

## Features

Super simple self-hosted Hydro Ottawa TOU rate API!

Simply sumbit a GET request at /rate and receive the current rate!

Can also pass in a dateTime query string to fetch the rate for any near date and time.


## Requirements

Docker or .NET 8.


## Installation

Grab it from docker hub:

``` docker-compose.yaml
services:
  hydro-ottawa-helper:
    container_name: hydro-ottawa-helper
    image: mattmckenzy/hydro-ottawa-helper:latest
    environment:
      - TZ=America/Toronto
```


## Known Issues

Nothing at the moment.


## Future Work

Nothing Planned.


## Release Notes

### 1.1.1

- Made Swagger UI default landing page.
