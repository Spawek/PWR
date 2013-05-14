function [ predictions ] = forecast_road_speed( training_data, work_data )

%% constants declaration
backward_data_usage_depth = 4; % app will use actual data this no backward
road_you_need_speed_on = 3;

% no of times that every nauron will be recalculated
% (neurons are recalculated in random permutation)
learning_iterations = 500;

%% fix data taken from speed.xls
training_data = fix_input_data(training_data);

%% calculating helpers
no_of_roads = size(training_data, 1);
training_data_points_no = size(training_data, 2) - (backward_data_usage_depth + 1);
probe_size = no_of_roads * (backward_data_usage_depth + 1);

%% creating 'random' weights vector
% weights vector is rand in range [0, 2/probe_size]
w = 2 / probe_size * rand(probe_size, 1);

%% creating probes from training data
% each column is 1 input point, each for loop iteration adds 1 column
probes = generate_probes(training_data, backward_data_usage_depth);

%% getting training_results arr from training data
training_results = training_data(road_you_need_speed_on, backward_data_usage_depth+2:end)';

%% network learning!
for iterations = 1:learning_iterations*probe_size;
    w = make_learning_step(w, probes, training_results);
end

%% tests
predictions = make_predictions(w, probes);
abs_error = abs((predictions - training_results));

predictions = [zeros(backward_data_usage_depth+1,1);predictions];

clf
subplot(2,1,1)
plot(predictions, 'r')
hold on
plot(training_data(road_you_need_speed_on,:),'b')

subplot(2,1,2)
plot(abs_error);

end
