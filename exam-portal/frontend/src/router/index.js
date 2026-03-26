import { createRouter, createWebHistory } from 'vue-router'
import ExamPage from '../views/ExamPage.vue'
import ResultPage from '../views/ResultPage.vue'
import ScorePage from '../views/ScorePage.vue'
import HistoryPage from '../views/HistoryPage.vue'

const router = createRouter({
  history: createWebHistory(),
  routes: [
    {
      path: '/',
      name: 'exam',
      component: ExamPage
    },
    {
      path: '/result',
      name: 'result',
      component: ResultPage
    },
    {
      path: '/score/:id',
      name: 'scoreDetail',
      component: ScorePage
    },
    {
      path: '/history',
      name: 'history',
      component: HistoryPage
    }
  ]
})

export default router