.PHONY: build run stop remove

all: build

build:
	docker build -t zhobot -f Dockerfile .

run:
	docker run --name zhobot --restart always -d zhobot

stop:
	docker stop zhobot

remove:
	docker rm zhobot
