<template><mainLayout>asdasd</mainLayout></template>

<script setup lang="ts">
import ProjectService from '@/lib/application/services/projectService'
import { useRoute, useRouter } from 'vue-router'
import mainLayout from '../../layouts/mainLayout.vue'

const route = useRoute()
const router = useRouter()

const projectId = Number(route.params.id)
</script>
