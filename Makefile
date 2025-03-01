.PHONY: caddy-fmt

caddy-fmt:
	docker exec -it lyzer-web caddy fmt /etc/caddy/Caddyfile --overwrite
