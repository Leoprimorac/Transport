<template>
  <router-view></router-view>
</template>

<script>
export default {
  created() {
    if (!this.$store.user) {
      this.$router.push("/");
    }
  },
};
</script>
