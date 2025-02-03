.PHONY: requirements
requirements:
	uv pip compile pyproject.toml --no-strip-extras --no-annotate -o requirements.txt
	
.PHONY: serve
serve:
	uv run mkdocs serve

.PHONY:
build:
	uv run mkdocs build
