FROM node as vite-app

ARG VITE_JWT_SECRET
ENV VITE_JWT_SECRET=$VITE_JWT_SECRET

ARG VITE_API_URL
ENV VITE_API_URL=$VITE_API_URL

WORKDIR /app/client

COPY frontend/package*.json ./

RUN npm install

COPY frontend/ ./

RUN npm run build

FROM nginx:alpine

WORKDIR /usr/share/nginx/

RUN rm -rf html && mkdir html

WORKDIR /

COPY frontend/nginx.conf /etc/nginx
COPY --from=vite-app ./app/client/dist /usr/share/nginx/html

ENTRYPOINT ["nginx", "-g", "daemon off;"]
