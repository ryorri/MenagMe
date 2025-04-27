import './assets/main.css'
import 'bootstrap/dist/css/bootstrap.css'

import { createApp } from 'vue'
import { createPinia } from 'pinia'

import App from './App.vue'
import router from '@/lib/application/router/index'
import { MenagMeWebApi } from './backend/MenagMeWebApi'

const backend = new MenagMeWebApi('http://localhost:5097')

const app = createApp(App)

app.use(createPinia())
app.use(router)

app.config.globalProperties.$apiClient = backend

app.mount('#app')
export { backend as Backend }
