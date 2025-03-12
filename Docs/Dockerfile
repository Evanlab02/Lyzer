FROM python:3.13.2-alpine3.21

ENV UV_SYSTEM_PYTHON 1
COPY --from=ghcr.io/astral-sh/uv:latest /uv /uvx /bin/

WORKDIR /app
COPY requirements.txt /app/requirements.txt
RUN uv pip install -r requirements.txt

COPY . /app

CMD ["mkdocs", "serve", "-a", "0.0.0.0:8000"]