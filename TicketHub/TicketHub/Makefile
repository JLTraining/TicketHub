DOCKER_COMPOSE ?= docker-compose

up:
	$(DOCKER_COMPOSE) up --remove-orphans -d
.PHONY: up

clean:
	$(DOCKER_COMPOSE) rm --force --stop
.PHONY: clean

build:
	$(DOCKER_COMPOSE) build
.PHONY: build

ps:
	$(DOCKER_COMPOSE) ps
.PHONY: ps

migrate:
	dotnet ef migrations add
.PHONY: migrate