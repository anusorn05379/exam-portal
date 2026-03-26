<script setup>
import { ref, onMounted } from 'vue'
import { useRouter } from 'vue-router'
import { getExams } from '../services/examService'

const router = useRouter()
const exams = ref([])
const loading = ref(true)
const error = ref('')

onMounted(async () => {
    try {
        const data = await getExams()
        exams.value = data
    } catch (err) {
        error.value = err.response?.data || 'ไม่สามารถดึงข้อมูลประวัติการสอบได้'
    } finally {
        loading.value = false
    }
})

const goHome = () => {
    router.push('/')
}

const viewDetail = (id) => {
    router.push(`/score/${id}`)
}
</script>

<template>
  <div class="page">
    <div class="card">
      <h1>ประวัติการสอบทั้งหมด</h1>

      <div v-if="loading" class="info-text">
        กำลังโหลดข้อมูล...
      </div>

      <div v-else-if="error" class="error-text">
        {{ error }}
      </div>

      <template v-else>
        <div v-if="exams.length === 0" class="info-text">
          ยังไม่มีข้อมูลการทำข้อสอบ
        </div>
        
        <div 
          v-for="exam in exams" 
          :key="exam.submissionId" 
          class="question-card" 
          style="display: flex; justify-content: space-between; align-items: center;"
        >
          <div>
            <p class="question-title" style="margin-bottom: 4px;">คุณ {{ exam.fullName }}</p>
            <div style="font-size: 14px; color: #555;">
              <p style="margin: 4px 0;">คะแนน: <strong>{{ exam.score }}/{{ exam.totalQuestions }}</strong></p>
              <p style="margin: 0;">วันที่: {{ new Date(exam.submittedAt).toLocaleString('th-TH') }}</p>
            </div>
          </div>
          <button class="primary-btn" @click="viewDetail(exam.submissionId)">
            ดูรายละเอียด
          </button>
        </div>
      </template>

      <div style="margin-top: 24px;">
        <button class="secondary-btn" style="width: 100%; border: 1px solid #bbb; padding: 10px; border-radius: 6px; cursor: pointer;" @click="goHome">
          กลับหน้าหลัก
        </button>
      </div>
    </div>
  </div>
</template>