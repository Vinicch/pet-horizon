.PHONY: api ui
.SILENT:

up:
	docker compose up -d

down:
	docker compose down --remove-orphans

api: up
	cd api && dotnet watch run

ui:
	cd ui && yarn serve
