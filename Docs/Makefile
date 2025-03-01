.PHONY: requirements
requirements:
	uv pip compile requirements.in --no-annotate -o requirements.txt
	
.PHONY: serve
serve:
	uv run mkdocs serve

.PHONY: build
build:
	uv run mkdocs build
