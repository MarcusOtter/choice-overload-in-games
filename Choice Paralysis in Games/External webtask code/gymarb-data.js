module.exports = function(context, callback) {
  // Get data
  context.storage.get(function(getError, data) {
    // Ensure nothing is wrong with the data
    if (getError) {
      return callback(getError);
    }
    data = data || { examinations: [] };
    if (!data.examinations) {
      data.examinations = [];
    }

    var newExamination = JSON.parse(context.query.data);
    data.examinations.push(newExamination);

    // Save the data
    context.storage.set(data, { force: 1 }, function(setError) {
      if (setError) {
        return callback(setError);
      }
    });

    callback(null, data);
  });
};
