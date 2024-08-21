[<img alt="GitHub Release" src="https://img.shields.io/github/v/release/mattmckenzy/hydro-ottawa-helper?logo=github&color=forestgreen">](https://github.com/mattmckenzy/hydro-ottawa-helper)
[<img alt="Docker Release" src="https://img.shields.io/docker/v/mattmckenzy/hydro-ottawa-helper/latest?logo=docker">](https://hub.docker.com/r/mattmckenzy/hydro-ottawa-helper)

# Hydro Ottawa Helper

## Features

Super simple self-hosted Hydro Ottawa TOU rate and cost API...

Simply sumbit a GET request at /rate and receive the current rate!

You can also submit a GET request at /cost and receive a total cost including charges and HST!

Both endpoints can also receive a dateTime query string to fetch the rate or total cost for any near date and time. Keep in mind that total cost is always calculated with current charges and HST.

## Requirements

Docker or .NET 8.


## Installation

Grab it from Docker Hub:

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

### 1.2.0

- Added a new cost endpoint that includes charges and HST.
