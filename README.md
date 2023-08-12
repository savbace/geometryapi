# Geometry.Api

## How to start the app?

### Run using docker-compose

1. Start application with dependencies:

```bash
cd src/Geometry.Api
docker-compose up -d
```

2. Navigate http://localhost:5000/swagger.

3. Stop application.

```bash
docker-compose down
```

### Seed DB with random rectangles

Call

```
POST /api/rectangles/seed?count=200
```
