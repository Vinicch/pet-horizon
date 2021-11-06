.SILENT:

up:
	docker compose up -d

down:
	docker compose down --remove-orphans

client:
	cd ui && yarn serve

serve:
	cd server && dotnet run
