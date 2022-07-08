FROM node:latest as build
WORKDIR /app
COPY . .
RUN npm ci
RUN npm build

FROM nginx
COPY --from=build /app/build /usr/share/nginx/html
COPY nginx.conf /etc/nginx/nginx.conf