import Vue from 'vue'
import ToastedPlugin from 'vue-toasted'

// utilizando biblioteca de ícones do fontawesome
Vue.use(ToastedPlugin, {
    iconPack: 'fontawesome',
    duration: 4000
})

Vue.toasted.register(
    'defaultSuccess',
    'Operation successfully completed!"',
    { type: 'success', icon: 'check'}
)

Vue.toasted.register(
    'defaultError',
    payload => !payload.msg  ? 'Unexpected Error!' : payload.msg,
    { type: 'error', icon: 'times'}
)

Vue.toasted.register(
    'defaultInfo',
    'download completed',
    { type: 'info', icon: 'info', className: 'toast-info' }
)

Vue.toasted.register(
    'downloadCompleted',
    payload => !payload.msg  ? 'Unexpected Error!' : payload.msg,
    { type: 'success', icon: 'hourglass_full'}
)