<template #label>
    <div class="mb-1">
        <span class="redColor" v-if="mandatoryIcon">* </span> {{ name }} 
    </div>
</template>

<script>
    export default {
        name: 'LabelInput',
        props: {
            name: {
                type: String
            },
            mandatoryIcon: {
                type: Boolean,
                default: true
            }
        }
    }
</script>

<style  scoped>
.redColor {
    color: red;
}
</style>