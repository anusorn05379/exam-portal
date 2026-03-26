<script setup>
import { useRouter } from 'vue-router'
import ResultSummary from '../components/ResultSummary.vue'
import { useExamStore } from '../stores/exam'

const store = useExamStore()
const router = useRouter()
const handleReset = () => {
    store.resetExam()
    router.push('/')
}
</script>


<template>
    <div class="page">
        <div class="card">
            <h1>IT 10-2</h1>

            <div v-if="!store.result" class="info-text">
                ไม่พบผลสอบ
            </div>

            <template v-else>
                <ResultSummary :result="store.result" />

                <button class="primary-btn" @click="handleReset">
                    สอบอีกครั้ง
                </button>
                <button class="secondary-btn" style="margin-left: 10px; margin-top: 10px;" @click="router.push(`/score/${store.result.submissionId}`)">
                    ดูรายละเอียดการสอบ
                </button>
            </template>
        </div>
    </div>
</template>
