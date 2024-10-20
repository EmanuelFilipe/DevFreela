const { defineConfig } = require('@vue/cli-service')
module.exports = defineConfig({
  transpileDependencies: [
    'vuetify'
  ],
  // habilitando o debbuger
  configureWebpack: {
    devtool: 'source-map'
  }
})
