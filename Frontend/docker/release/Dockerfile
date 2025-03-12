FROM node:22.13-alpine3.21 AS build

COPY package.json /build/package.json
COPY package-lock.json /build/package-lock.json
WORKDIR /build
RUN npm install
COPY . /build
RUN npm run build

FROM caddy:2.9.1-alpine AS final

COPY --from=build /build/dist /var/www/html
COPY Caddyfile /etc/caddy/Caddyfile

EXPOSE 80
