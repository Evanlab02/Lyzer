/// <reference types="vitest" />
import { defineConfig } from "vite";
import react from "@vitejs/plugin-react-swc";

// https://vite.dev/config/
export default defineConfig({
	plugins: [react()],
	server: {
		open: "/"
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
