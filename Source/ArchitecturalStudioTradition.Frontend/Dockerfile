###########################
###### BUILD CONTEXT ######
###########################

FROM node:14 as init

WORKDIR /app

COPY . /app/
RUN --mount=type=cache,target=/root/.npm npm ci

####################################
###### UNIT TESTS ######
####################################
FROM init as test
RUN npm run pre-build

####################################
###### BUILD SELFHOST ######
####################################
FROM test as buildSelfHostContainer
RUN npm run build:selfhost

FROM nginx:alpine as ArchitecturalStudioTradition.Frontend
COPY nginx.conf /etc/nginx/nginx.conf
WORKDIR /usr/share/nginx/html
COPY --from=buildSelfHostContainer /app/dist/selfhost/ .

####################################
###### BUILD SSR ######
####################################
FROM init as buildSsrContainer
RUN npm run build:ssr

FROM init as ArchitecturalStudioTradition.Frontend.Ssr
COPY --from=buildSsrContainer /app/dist/ ./dist
ENV NODE_ENV=production
CMD [ "node", "dist/server/main" ]
