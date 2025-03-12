FROM node:22.13-alpine3.21

WORKDIR /app

# Install dependencies first (leveraging Docker cache)
COPY package.json /app/package.json
COPY package-lock.json /app/package-lock.json
RUN npm install
COPY . /app/

# Set environment
ENV NODE_ENV=development

# Expose Vite dev server port
EXPOSE 5173

# Default command
CMD ["npm", "run", "dev:docker"]
