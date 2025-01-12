FROM node as frontend
WORKDIR /app
EXPOSE 5000
COPY package.json .
RUN npm install --legacy-peer-deps
COPY . .
RUN npm run build

FROM ngnix:stable-alpine as server
COPY --from=server /usr/src/app/dist /usr/share/nginx/html
COPY nginx.conf /etc/nginx/conf.d/default.conf

CMD ["nginx", "-g", "daemon off;"]
