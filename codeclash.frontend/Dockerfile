FROM node

WORKDIR /app

EXPOSE 5000
EXPOSE 5001

COPY package.json .

RUN npm install --legacy-peer-deps
RUN npm install -g serve

COPY . .

RUN npm run build

CMD ["npm", "run", "start"]
