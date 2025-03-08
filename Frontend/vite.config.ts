/* eslint-disable @typescript-eslint/no-unsafe-member-access */
/// <reference types="vitest" />
/// <reference types="node" />
import { defineConfig, loadEnv } from "vite";
import react from "@vitejs/plugin-react-swc";

// https://vite.dev/config/
export default defineConfig(({ mode }) => {
	// Load env file based on `mode` in the current directory.
	// Set the third parameter to '' to load all env regardless of the `VITE_` prefix.
	const env = loadEnv(mode, process.cwd(), '');

	// API target - default to localhost if not specified
	const apiTarget = env.API_TARGET || 'http://localhost:8000';
	console.log(`Using API target: ${apiTarget}`);
	
	return {
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
					target: apiTarget,
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
	};
});
