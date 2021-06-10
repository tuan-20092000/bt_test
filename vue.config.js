module.exports = {
  devServer: {
    proxy: "https://localhost:44384/",
  },

  lintOnSave: false,

  transpileDependencies: ["vuetify"],
};
