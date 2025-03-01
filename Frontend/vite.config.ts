/* eslint-disable @typescript-eslint/no-unsafe-member-access */
/// <reference types="vitest" />
import { defineConfig } from "vite";
import react from "@vitejs/plugin-react-swc";

// https://vite.dev/config/
export default defineConfig({
	plugins: [react()],
	build: {
		rollupOptions: {
		  external: ["src/tests/**", "src/setupTests.ts"],
		},
	},
	server: {
		open: "/",
		proxy: {
			"/apis/lyzer": {
				target: "http://localhost:8000",
				changeOrigin: true,
				rewrite: (path) => path.replace("/apis/lyzer", ""),
				configure: (proxy) => {
					proxy.on("proxyReq", (_, req) => {
						console.log("Sending request to the target:", req.method, req.url);
					});
					proxy.on("proxyRes", (proxyRes, req) => {
						console.log("Received response from the target:", proxyRes.statusCode, req.url);
					});
					proxy.on("error", (err) => {
						console.error("Proxy error", err);
					});
				}
			}
		}
	},
	css: {
		preprocessorOptions: {
			scss: {
				api: "modern"
			}
		}
	},
	test: {
		coverage: {
			enabled: true,
			provider: "v8"
		},
		setupFiles: ["./src/setupTests.ts"],
		workspace: [ 
		  { 
				extends: true, 
				test: { 
			  		environment: "jsdom", 
				}, 
			}, 
		],
	}
});
