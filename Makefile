.PHONY: caddy-fmt
caddy-fmt:
	docker exec -it lyzer-web caddy fmt /etc/caddy/Caddyfile --overwrite

.PHONY: up
up:
	docker compose up -d

.PHONY: dev
dev:
	docker compose watch

.PHONY: debug
debug:
	docker compose up

.PHONY: down
down:
	docker compose down

.PHONY: build
build:
	docker compose build

.PHONY: pull
pull:
	docker compose pull

.PHONY: staging
staging:
	docker compose -f compose.staging.yaml up -d --build

.PHONY: staging-down
staging-down:
	docker compose -f compose.staging.yaml down
