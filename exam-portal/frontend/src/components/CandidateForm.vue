<script setup>
defineProps({
  modelValue:{
    type: String,
    default: ''
  }
})

defineEmits(['update:modelValue'])

</script>
<template>
  <div class="form-group">
    <label for="fullName">ชื่อ-สกุล</label>
    <input
      id="fullName"
      :value="modelValue"
      type="text"
      placeholder="กรอกชื่อ-สกุล"
      @input="$emit('update:modelValue', $event.target.value)"
    />
  </div>
</template>
