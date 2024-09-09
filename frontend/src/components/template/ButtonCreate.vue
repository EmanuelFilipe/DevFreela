<template>
  <div class="d-flex justify-content-end">
    <router-link
      :to="url"
      tag="a"
      class="text-right btn-lg btn btn-outline-primary fa fa-plus"
    >
      <strong> New</strong>
    </router-link>
  </div>
</template>

<script>
export default {
  name: "ButtonCreate",
  props: ['url']
};
</script>