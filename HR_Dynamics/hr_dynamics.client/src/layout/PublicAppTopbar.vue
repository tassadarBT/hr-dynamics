<script setup>
import { computed } from 'vue';
import { useLayout } from '@/layout/composables/layout';
import { useRouter } from 'vue-router';

const { layoutConfig } = useLayout();
const router = useRouter();

const logoUrl = computed(() => {
    return `/layout/images/${layoutConfig.darkTheme.value ? 'logo-white' : 'logo-dark'}.svg`;
});
</script>

<template>
    <div class="layout-topbar">
        <router-link to="/landing" class="layout-topbar-logo">
            <img :src="logoUrl" alt="logo" style="height: 75px;" />
        </router-link>
    </div>
</template>

<style lang="scss" scoped></style>
